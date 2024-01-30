LookupServiceClient PoC
===

The purpose of this client PoC (proof-of-concept) is to demonstrate that the KOMBIT published OIOIDWS WSC library for .NET can not be used with NemLog-ins STS currently.

By default the WCF client port for .NET/.NET Standard 2.0 from Microsoft uses SOAP v1.2 (see [WSTrustChannelSecurityTokenProvider.cs: line 236](https://github.com/dotnet/wcf/blob/release/4.10/src/System.ServiceModel.Federation/src/System/ServiceModel/Federation/WSTrustChannelSecurityTokenProvider.cs#L236)) which is not compatible with the OIO IDWS v1.0 profile requirements of using SOAP v1.1. It is not possible using configuration to override this behaviour.

Once this issue has been resolved then support for WS-Policy v1.2, with sub-requirements to addressing (scenario URL), UUID format and AppliesTo must be addressed.

Running the contained application will result in a HTTP/415 response from the NemLog-in STS. Log traces performed from the application ran on:

- Windows: [docs/traces/windows.log](docs/traces/windows.log)
- macOS: [docs/traces/macos.log](docs/traces/macos.log)
