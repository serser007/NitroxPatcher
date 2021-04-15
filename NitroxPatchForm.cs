using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using NitroxPatch.Properties;
using WMPLib;

namespace NitroxPatch
{
    public partial class NitroxPatchForm : Form
    {
        private const string SubnauticaAppFileName = "Subnautica.exe";
        private const string NitroxAppFileName = "NitroxLauncher.exe";
        private const string PatchedNitroxAppFileName = "PatchedNitroxLauncher.exe";
        private const string CrackSteamFileName = "steam_api64.dll";
        private const string SubnauticaDataDirectoryName = "Subnautica_Data";
        private const string MagicDirectoryName = ".egstore";
        private const string FileSelectErrorTemplate = "There is no {0} app on selected path";


        private string SubnauticaFile => Path.Combine(subnauticaPathLabel.Text, SubnauticaAppFileName);
        private string SubnauticaFileReplaced => Path.Combine(subnauticaPathLabel.Text, "." + SubnauticaAppFileName);
        private string DataDirectory => Path.Combine(subnauticaPathLabel.Text, SubnauticaDataDirectoryName);
        private string HiddenDataDirectory => Path.Combine(subnauticaPathLabel.Text, "." + SubnauticaDataDirectoryName);
        private string MagicDirectory => Path.Combine(subnauticaPathLabel.Text, MagicDirectoryName);
        private string NitroxFile => Path.Combine(nitroxPathLabel.Text, NitroxAppFileName);
        private string NitroxFilePatched => Path.Combine(nitroxPathLabel.Text, PatchedNitroxAppFileName);
        private string CrackSteamFile => Path.Combine(subnauticaPathLabel.Text, CrackSteamFileName);
        private string CrackSteamFileHidden => Path.Combine(subnauticaPathLabel.Text, "." + CrackSteamFileName);
        private string DataDirectoryLinked => Path.Combine(imitationPathLabel.Text, SubnauticaDataDirectoryName);
        private string MagicDirectoryDedicated => Path.Combine(imitationPathLabel.Text, MagicDirectoryName);
        private string PathFile => Path.Combine(nitroxPathLabel.Text, "path.txt");

        private Point lastMousePosition;
        private bool isMoving;
        private string wmpTempFile;

        public NitroxPatchForm()
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
        
        private void imitationPathSelectButton_Click(object sender, EventArgs e)
        {
            SelectPath(
                IsEmptyPath,
                () => MessageBox.Show("Directory must be empty"), 
                s => imitationPathLabel.Text = s,
                "Imitation Path...");
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
            var isImitationOk = !imitateSubnauticaCheckBox.Checked || IsEmptyPath(imitationPathLabel.Text);
            if (!isSubnauticaOk)
                MessageBox.Show(string.Format(FileSelectErrorTemplate, "Subnautica"));
            if (!isNitroxOk)
                MessageBox.Show(string.Format(FileSelectErrorTemplate, "Nitrox"));
            if (!isImitationOk)
                MessageBox.Show("Imitation directory myst be empty");
            return isSubnauticaOk && isNitroxOk && isImitationOk;
        }

        private static bool IsSubnauticaPath(string path)
        {
            return File.Exists(Path.Combine(path, SubnauticaAppFileName));
        }

        private static bool IsEmptyPath(string path)
        {
            return  Directory.Exists(path) && !Directory.EnumerateFiles(path).Any();
        }

        private static bool IsNitroxPath(string path)
        {
            return File.Exists(Path.Combine(path, NitroxAppFileName));
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

            CleanSubnauticaDir();
            if (IsWindows10() && imitateSubnauticaCheckBox.Checked)
                PatchWindows10();
            else
                PatchUniversal();
            DoSuccessExit();
        }

        private static bool IsWindows10()
        {
            var registry = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            var productName = (string)registry?.GetValue("ProductName");
            return productName?.StartsWith("Windows 10") == true;
        }

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void CleanSubnauticaDir()
        {
            if (File.Exists(NitroxFilePatched))
                File.Delete(NitroxFilePatched);

            if (File.Exists(SubnauticaFileReplaced) && new FileInfo(SubnauticaFile).Length < 20000)
                File.Replace(SubnauticaFileReplaced, SubnauticaFile, SubnauticaFile + ".old");

            if (File.Exists(CrackSteamFileHidden) && !File.Exists(CrackSteamFile))
                File.Move(CrackSteamFileHidden, CrackSteamFile);

            if (Directory.Exists(HiddenDataDirectory) && !Directory.Exists(DataDirectory))
                Directory.Move(HiddenDataDirectory, DataDirectory);

            if (Directory.Exists(MagicDirectory))
                Directory.Delete(MagicDirectory);
        }

        private void PatchWindows10()
        {
            if (!Directory.Exists(imitationPathLabel.Text))
                Directory.CreateDirectory(imitationPathLabel.Text);
            if (!Directory.Exists(MagicDirectoryDedicated))
                Directory.CreateDirectory(MagicDirectoryDedicated);
            BuildLinkRenameApp(imitationPathLabel.Text, SubnauticaAppFileName, SubnauticaFile, null, null, null, null, null, false);
            Process.Start("cmd.exe", $"/C mklink \"{DataDirectoryLinked}\" \"{DataDirectory}\"")?.WaitForExit();
            File.WriteAllText(PathFile, imitationPathLabel.Text);
        }

        private void PatchUniversal()
        {
            if (!File.Exists(SubnauticaFileReplaced))
                File.Move(SubnauticaFile, SubnauticaFileReplaced);
            if (!Directory.Exists(MagicDirectory))
                Directory.CreateDirectory(MagicDirectory);
            BuildLinkRenameApp(subnauticaPathLabel.Text, SubnauticaAppFileName, SubnauticaFileReplaced, CrackSteamFileHidden, CrackSteamFile, DataDirectory, HiddenDataDirectory, "NitroxLauncher", true);
            BuildLinkRenameApp(nitroxPathLabel.Text, PatchedNitroxAppFileName, NitroxFile, CrackSteamFile, CrackSteamFileHidden, HiddenDataDirectory, DataDirectory, ".Subnautica", false);
            File.WriteAllText(PathFile, subnauticaPathLabel.Text);
        }

        private void DoSuccessExit()
        {
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

            if (renameFrom != null && renameTo != null)
            {
                il.BeginExceptionBlock();
                il.Emit(OpCodes.Ldstr, renameFrom);
                il.Emit(OpCodes.Ldstr, renameTo);
                il.EmitCall(OpCodes.Call, methodFileRename ?? throw new InvalidOperationException(), null);
                il.BeginCatchBlock(typeof(Exception));
                il.EndExceptionBlock();
            }

            if (directoryRenameFrom != null && directoryRenameTo != null)
            {
                il.BeginExceptionBlock();
                il.Emit(OpCodes.Ldstr, directoryRenameFrom);
                il.Emit(OpCodes.Ldstr, directoryRenameTo);
                il.EmitCall(OpCodes.Call, methodDirectoryRename ?? throw new InvalidOperationException(), null);
                il.BeginCatchBlock(typeof(Exception));
                il.EndExceptionBlock();
            }

            if (differentProcess != null)
            {
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
                
            var isWindows10 = IsWindows10();
            imitateSubnauticaCheckBox.Visible = isWindows10;
            imitateSubnauticaCheckBox.CheckedChanged += (_, __) => UpdateUI();
            UpdateUI();
        }

        // ReSharper disable once InconsistentNaming
        private void UpdateUI()
        {
            imitationPathGroupBox.Visible = imitateSubnauticaCheckBox.Checked;
            UpdateHeight();
        }

        private void UpdateHeight()
        {
            Height = CalculateHeight();
        }

        private int CalculateHeight()
        {
            var staticHeight = label1.Height + nitroxPathGroupBox.Height + subnauticaPathGroupBox.Height + 60;
            var checkBoxHeight = imitateSubnauticaCheckBox.Visible ? imitateSubnauticaCheckBox.Height : 0;
            var imitationPathHeight = imitateSubnauticaCheckBox.Visible && imitateSubnauticaCheckBox.Checked ? imitationPathGroupBox.Height : 0;
            return staticHeight + checkBoxHeight + imitationPathHeight;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (File.Exists(wmpTempFile))
                File.Delete(wmpTempFile);
        }
    }
}
