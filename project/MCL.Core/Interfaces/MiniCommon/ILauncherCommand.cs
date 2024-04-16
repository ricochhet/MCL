using System.Threading.Tasks;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.MiniCommon;

public interface ILauncherCommand
{
    public abstract Task Init(string[] args, Config config);
}
