using System.Threading.Tasks;

namespace MCL.Core.Services.Interfaces;

public interface IDownloadService
{
    public static abstract Task<bool> Download(bool useLocalVersionManifest = false);
}
