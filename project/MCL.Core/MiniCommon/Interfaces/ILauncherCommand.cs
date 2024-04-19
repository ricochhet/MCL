using System.Threading.Tasks;
using MCL.Core.Launcher.Models;

namespace MCL.Core.MiniCommon.Interfaces;

public interface ILauncherCommand
{
    public abstract Task Init(string[] args, Settings settings);
}
