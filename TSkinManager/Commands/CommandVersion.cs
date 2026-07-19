using Rocket.API;
using System.Collections.Generic;
using System.Reflection;
using Tavstal.TLibrary.Helpers.Unturned;
// ReSharper disable UnusedType.Global

namespace Tavstal.TSkinManager.Commands
{
    /// <summary>
    /// RocketMod command that displays the plugin version and build date.
    /// Command: /vTSkinManager
    /// </summary>
    public class CommandVersion : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => ("v" + Assembly.GetExecutingAssembly().GetName().Name);
        public string Help => "Gets the version of the plugin";
        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "tskinmanager.commands.version" };

        /// <summary>
        /// Sends the plugin version and build date to the command caller.
        /// </summary>
        /// <param name="caller">The player or console executing the command.</param>
        /// <param name="command">Command arguments (unused).</param>
        public void Execute(IRocketPlayer caller, string[] command)
        {
            TSkinManager.Instance.SendPlainCommandReply(caller, "#########################################");
            TSkinManager.Instance.SendPlainCommandReply(caller, $"# Build Version: {TSkinManager.Version}");
            TSkinManager.Instance.SendPlainCommandReply(caller, $"# Build Date: {TSkinManager.BuildDate}");
            TSkinManager.Instance.SendPlainCommandReply(caller, "#########################################");
        }
    }
}
