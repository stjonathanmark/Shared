namespace Shared.Mailing;

public class EmailTemplateOptions
{
    public static string SectionName => "Mailing:Template";

    public string FromAddress { get; set; } = string.Empty;

    public string FromDisplayName { get; set; } = string.Empty;

    public string PlaceHolderBeginning { get; set; } = string.Empty;

    public string PlaceHolderEnding { get; set; } = string.Empty;

    public EmailTemplateLocation Location { get; set; } = EmailTemplateLocation.FileSystem;

    public FileTemplateOptions File { get; set; } = new();

    public CloudTemplateOptions Cloud { get; set; } = new();
}

public class CloudTemplateOptions
{
    public string BucketName { get; set; } = string.Empty;
}

public class FileTemplateOptions
{
    public string BasePath { get; set; } = string.Empty;

    public string HtmlPath { get; set; } = string.Empty;

    public string TextPath { get; set; } = string.Empty;
}