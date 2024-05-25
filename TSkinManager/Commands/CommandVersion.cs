using Rocket.API;
using System.Collections.Generic;
using System.Reflection;
using Tavstal.TLibrary.Helpers.Unturned;

namespace Tavstal.TSkinManager.Commands
{
    public class CommandVersion : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => ("v" + Assembly.GetExecutingAssembly().GetName().Name);
        public string Help => "Gets the version of the plugin";
        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "tskinmanager.commands.version" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            TSkinManager.Instance.SendPlainCommandReply(caller, "#########################################");
            TSkinManager.Instance.SendPlainCommandReply(caller, string.Format("# Build Version: {0}", TSkinManager.Version));
            TSkinManager.Instance.SendPlainCommandReply(caller, string.Format("# Build Date: {0}", TSkinManager.BuildDate));
            TSkinManager.Instance.SendPlainCommandReply(caller, "#########################################");
        }
    }
}
