using System.Threading.Tasks;

namespace MCL.Core.Interfaces.Minecraft;

public interface IDownloadService
{
    public static abstract Task<bool> Download();
}
