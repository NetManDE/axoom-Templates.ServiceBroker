# AXOOM Service Broker Template

This template helps you implement a Service Broker according to the [Open Service Broker API](https://www.openservicebrokerapi.org/).

# Using the template

Download and install the [.NET Core SDK](https://www.microsoft.com/net/download) on your machine. You can then install the template by running the following:

    dotnet new --install Axoom.Templates.ServiceBroker::*

To use the template to create a new project:

    dotnet new axoom-servicebroker --name "MyVendor.ServiceBroker" --serviceName myvendor-servicebroker --friendlyName "My Service Broker" --vendorDomain example.com --description "my description"
    cd MyVendor.ServiceBroker

In the commands above replace
- `MyVendor.ServiceBroker` with the .NET namespace you wish to use,
- `myvendor-servicebroker` with the name of your company and service broker using only lowercase letters and hyphens,
- `My Service Broker` with the full name of your service broker,
- `example.com` with the public domain of your company and
- `my description` with a brief (single sentence) description of the service broker.

You can now open the generated project using your favorite IDE. We recommend [Visual Studio](https://www.visualstudio.com/downloads/), [Visual Studio Code](https://code.visualstudio.com/Download) or [Rider](https://www.jetbrains.com/rider/).
