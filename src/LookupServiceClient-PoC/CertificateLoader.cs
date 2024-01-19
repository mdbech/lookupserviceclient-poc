using System.Security.Cryptography.X509Certificates;

namespace LookupServiceClient_PoC;

internal class CertificateLoader
{
    internal static X509Certificate2 ReadCertificate(string resourceFileName)
    {
        return new X509Certificate2(ReadResourceBytes(resourceFileName));
    }

    internal static X509Certificate2 ReadPkcs12(string resourceFileName, string password)
    {
        return new X509Certificate2(ReadResourceBytes(resourceFileName), password);
    }

    private static byte[] ReadResourceBytes(string resourceFileName)
    {
        using var stream = typeof(CertificateLoader).Assembly.GetManifestResourceStream(resourceFileName);

        if (stream == null)
        {
            throw new ArgumentException($"Couldn't read manifest resource stream for {resourceFileName}",
                nameof(resourceFileName));
        }

        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        return bytes;
    }
}