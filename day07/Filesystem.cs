namespace Filesystem;

public record FileNode(string Name, int Size);
public record DirNode(string Name, DirNode? Parent, Dictionary<String,DirNode> Dirs, Dictionary<String,FileNode> Files);
