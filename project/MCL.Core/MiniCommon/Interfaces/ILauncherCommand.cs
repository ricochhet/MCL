using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.MiniCommon.Interfaces;

public interface ILauncherCommand
{
    public abstract Task Init(string[] args, Settings settings);
}
