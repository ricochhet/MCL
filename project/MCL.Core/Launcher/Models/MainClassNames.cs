namespace MCL.Core.Launcher.Models;

public class MainClassNames
{
    public string Vanilla { get; set; } = "net.minecraft.client.main.Main";
    public string Fabric { get; set; } = "net.fabricmc.loader.impl.launch.knot.KnotClient";
    public string Quilt { get; set; } = "org.quiltmc.loader.impl.launch.knot.KnotClient";
}
