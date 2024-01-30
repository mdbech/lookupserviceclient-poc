using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using Digst.OioIdws.CommonCore;
using Digst.OioIdws.OioWsTrustCore;
using log4net.Config;
using LookupServiceClient_PoC;

XmlConfigurator.Configure();

try
{
    var stsTokenServiceConfiguration = PreProductionConfiguration();
    var stsTokenService = new StsTokenServiceCache(stsTokenServiceConfiguration);
    var securityToken = (GenericXmlSecurityToken)stsTokenService.GetToken();
    Console.WriteLine("Direct token: " + securityToken.TokenXml.OuterXml);
}
catch (Exception ex)
{
    Console.ReadKey();
}

return;

StsTokenServiceConfiguration PreProductionConfiguration()
{
    var wscCertificate = CertificateLoader.ReadPkcs12("LookupServiceClient_PoC.Resources.LookupService.Client-PreProd.p12", "4f?XfNCXCR12");
    var stsCertificate = CertificateLoader.ReadCertificate("LookupServiceClient_PoC.Resources.sts-cert-inttest-13-03-2023.pem");
    var wspCertificate = CertificateLoader.ReadCertificate("LookupServiceClient_PoC.Resources.NemLog-in_LookupService_Test.pem");
    var stsConfiguration = new StsConfiguration(
        "https://SecureTokenService.test-devtest4-nemlog-in.dk/SecurityTokenService.svc",
        "https://saml.test-devtest4-nemlog-in.dk",
        "94827547",
        stsCertificate
        );
    var wspConfiguration = new WspConfiguration(
        "https://lookupservice.test-devtest4-nemlog-in.dk/LookupService.svc",
        "https://saml.wsp.lookupservice.test-devtest4-nemlog-in.dk",
        EnvelopeVersion.Soap11,
        wspCertificate
        );
    
    return new StsTokenServiceConfiguration(stsConfiguration, wspConfiguration, wscCertificate)
    {
        SendTimeout = TimeSpan.FromMinutes(3),
        TokenLifeTimeInMinutes = 1,
        MaxReceivedMessageSize = 1048576,
        IncludeLibertyHeader = false,
        CacheClockSkew = TimeSpan.FromSeconds(60),
        StsCertificateAuthentication = { 
            CertificateValidationMode = X509CertificateValidationMode.None,
            RevocationMode = X509RevocationMode.NoCheck
        },
        WspCertificateAuthentication = {
            CertificateValidationMode = X509CertificateValidationMode.None,
            RevocationMode = X509RevocationMode.NoCheck
        }
    };
}