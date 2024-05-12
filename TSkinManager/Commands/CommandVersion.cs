using Rocket.API;
using System.Collections.Generic;
using System.Reflection;
using Logger = Tavstal.TSkinManager.Helpers.LoggerHelper;

namespace Tavstal.TSkinManager.Commands
{
    public class CommandVersion : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => ("v" + Assembly.GetExecutingAssembly().GetName().Name);
        public string Help => "Gets the version of the plugin";
        public string Syntax => "";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "tskinmanager.version" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            Logger.Log("#########################################");
            Logger.Log(string.Format("# Build Version: {0}", TSkinManager.Instance._Version));
            Logger.Log(string.Format("# Build Date: {0}", TSkinManager.Instance._BuildDate));
            Logger.Log("#########################################");
        }
    }
}
