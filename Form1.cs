using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using NitroxPatch.Properties;
using WMPLib;

namespace NitroxPatch
{
    public partial class Form1 : Form
    {
        private const string SubnauticaAppFile = "Subnautica.exe";
        private const string NitroxAppFile = "NitroxLauncher.exe";
        private const string PatchedNitroxAppFile = "PatchedNitroxLauncher.exe";
        private const string CrackSteamFile = "steam_api64.dll";
        private const string SubnauticaDataFile = "Subnautica_Data";
        private const string FileSelectErrorTemplate = "There is no {0} app on selected path";
        private const string MagicDir = ".egstore";

        private Point lastMousePosition;
        private bool isMoving;
        private string wmpTempFile;

        public Form1()
        {
            InitializeComponent();
        }

        private void subnauticaPathSelectButton_Click(object sender, EventArgs e)
        {
            SelectPath(
                IsSubnauticaPath,
                () => MessageBox.Show(string.Format(FileSelectErrorTemplate, "Subnautica")), 
                s => subnauticaPathLabel.Text = s,
                "Subnautica...");
        }

        private void nitroxPathSelectButton_Click(object sender, EventArgs e)
        {
            SelectPath(
                IsNitroxPath,
                () => MessageBox.Show(string.Format(FileSelectErrorTemplate, "Subnautica")), 
                s => nitroxPathLabel.Text = s,
                "Nitrox...");
        }
        
        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SelectPath(Func<string, bool> checker, Action onCheckFail, Action<string> onCheckSuccess, string caption)
        {
            folderBrowser.Description = caption;
            if (folderBrowser.ShowDialog() != DialogResult.OK) return;
            if (!checker(folderBrowser.SelectedPath))
            {
                onCheckFail();
                return;
            }
            onCheckSuccess(folderBrowser.SelectedPath);
        }

        private bool IsPathsValid()
        {
            var isSubnauticaOk = IsSubnauticaPath(subnauticaPathLabel.Text);
            var isNitroxOk = IsNitroxPath(nitroxPathLabel.Text);
            if (!isSubnauticaOk)
                MessageBox.Show(string.Format(FileSelectErrorTemplate, "Subnautica"));
            if (!isNitroxOk)
                MessageBox.Show(string.Format(FileSelectErrorTemplate, "Nitrox"));
            return isSubnauticaOk && isNitroxOk;
        }

        private static bool IsSubnauticaPath(string path)
        {
            return File.Exists(Path.Combine(path, SubnauticaAppFile));
        }

        private static bool IsNitroxPath(string path)
        {
            return File.Exists(Path.Combine(path, NitroxAppFile));
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePosition = Cursor.Position;
            isMoving = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMoving)
                return;
            var delta = Cursor.Position - new Size(lastMousePosition);
            Location += new Size(delta);
            lastMousePosition = Cursor.Position;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void setupButton_Click(object sender, EventArgs e)
        {
            if(!IsPathsValid())
                return;
            var subnauticaFile = Path.Combine(subnauticaPathLabel.Text, SubnauticaAppFile);
            var subnauticaFileReplaced = Path.Combine(subnauticaPathLabel.Text, "." + SubnauticaAppFile);
            var nitroxFile = Path.Combine(nitroxPathLabel.Text, NitroxAppFile);
            var crackSteamFile = Path.Combine(subnauticaPathLabel.Text, CrackSteamFile);
            var crackSteamFileHidden = Path.Combine(subnauticaPathLabel.Text, "." + CrackSteamFile);
            var dataDirectory = Path.Combine(subnauticaPathLabel.Text, SubnauticaDataFile);
            var hiddenDataDirectory = Path.Combine(subnauticaPathLabel.Text, "." + SubnauticaDataFile);
            var magicDirectory = Path.Combine(subnauticaPathLabel.Text, MagicDir);

            if (!File.Exists(subnauticaFileReplaced))
                File.Move(subnauticaFile, subnauticaFileReplaced);
            if (!Directory.Exists(magicDirectory))
                Directory.CreateDirectory(magicDirectory);
            BuildLinkRenameApp(subnauticaPathLabel.Text, SubnauticaAppFile, subnauticaFileReplaced, crackSteamFileHidden, crackSteamFile, dataDirectory, hiddenDataDirectory, "NitroxLauncher", true);
            BuildLinkRenameApp(nitroxPathLabel.Text, PatchedNitroxAppFile, nitroxFile, crackSteamFile, crackSteamFileHidden, hiddenDataDirectory, dataDirectory, ".Subnautica", false);
            MessageBox.Show("Success!", "Good start!", MessageBoxButtons.OK, MessageBoxIcon.None);
            Application.Exit();
        }
        
        private void BuildLinkRenameApp(string path, string filename, string link, string renameFrom, string renameTo, string directoryRenameFrom, string directoryRenameTo, string differentProcess, bool kill)
        {
            var assemblyName = new AssemblyName(filename);
            var assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save, path);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("S", filename);
            var typeBuilder = moduleBuilder.DefineType("Program");
            
            var methodStartApp = typeof(Process).GetMethod("Start", new[] { typeof(string) });
            var methodFileRename = typeof(File).GetMethod("Move", new[] { typeof(string), typeof(string) });
            var methodDirectoryRename = typeof(Directory).GetMethod("Move", new[] { typeof(string), typeof(string) });
            var methodGetProcesses = typeof(Process).GetMethod("GetProcessesByName", new[] { typeof(string) });
            var methodKill = typeof(Process).GetMethod("Kill");
            var methodReadKey = typeof(Console).GetMethod("ReadKey", new Type[0]);
            var mainBuilder = typeBuilder.DefineMethod("Main", MethodAttributes.Static | MethodAttributes.Public, typeof(void), null);
            
            var il = mainBuilder.GetILGenerator();
            var ilLabel1 = il.DefineLabel();
            var ilLabel2 = il.DefineLabel();
            var i = il.DeclareLocal(typeof(int));
            var processes = il.DeclareLocal(typeof(Process[]));

            il.BeginExceptionBlock();
            il.Emit(OpCodes.Ldstr, renameFrom);
            il.Emit(OpCodes.Ldstr, renameTo);
            il.EmitCall(OpCodes.Call, methodFileRename ?? throw new InvalidOperationException(), null);
            il.BeginCatchBlock(typeof(Exception));
            il.EndExceptionBlock();
            il.BeginExceptionBlock();
            il.Emit(OpCodes.Ldstr, directoryRenameFrom);
            il.Emit(OpCodes.Ldstr, directoryRenameTo);
            il.EmitCall(OpCodes.Call, methodDirectoryRename ?? throw new InvalidOperationException(), null);
            il.BeginCatchBlock(typeof(Exception));
            il.EndExceptionBlock();

            il.Emit(OpCodes.Ldstr, differentProcess);
            il.EmitCall(OpCodes.Call, methodGetProcesses ?? throw new InvalidOperationException(), null);
            il.Emit(OpCodes.Stloc, processes);

            if (kill)
            {
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Stloc, i);
                il.MarkLabel(ilLabel1);

                //if (i >= processes.Length)
                //  break;
                il.Emit(OpCodes.Ldloc, i);
                il.Emit(OpCodes.Ldloc, processes);
                il.Emit(OpCodes.Ldlen);
                il.Emit(OpCodes.Bge, ilLabel2);

                // processes[i].Kill()
                il.Emit(OpCodes.Ldloc, processes);
                il.Emit(OpCodes.Ldloc, i);
                il.Emit(OpCodes.Ldelem, typeof(Process));
                il.EmitCall(OpCodes.Call, methodKill ?? throw new InvalidOperationException(), null);

                // i++;
                il.Emit(OpCodes.Ldloc, i);
                il.Emit(OpCodes.Ldc_I4_1);
                il.Emit(OpCodes.Add);
                il.Emit(OpCodes.Stloc, i);

                il.Emit(OpCodes.Br_S, ilLabel1);

              
            }
            else
            {
                il.Emit(OpCodes.Ldloc, processes);
                il.Emit(OpCodes.Ldlen);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Beq, ilLabel2);
                il.EmitWriteLine($"Please close {differentProcess} window");
                il.EmitCall(OpCodes.Call, methodReadKey ?? throw new InvalidOperationException(), null);
                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ret);
            }

            il.MarkLabel(ilLabel2);
            il.Emit(OpCodes.Ldstr, link);
            il.EmitCall(OpCodes.Call, methodStartApp ?? throw new InvalidOperationException(), null);
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ret);
            typeBuilder.CreateType();
            assemblyBuilder.SetEntryPoint(mainBuilder);
            assemblyBuilder.Save(filename);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wmpTempFile = Path.GetTempPath() + Guid.NewGuid() + ".mp3";
            var wmpData = Resources.PIRATE;
            File.WriteAllBytes(wmpTempFile, wmpData);

            var wmp = new WindowsMediaPlayer {URL = wmpTempFile};
            wmp.controls.play();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(wmpTempFile))
                File.Delete(wmpTempFile);
        }
    }
}
