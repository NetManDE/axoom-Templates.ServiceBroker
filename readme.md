# AXOOM Service Broker Template

[![NuGet package](https://img.shields.io/nuget/v/Axoom.Templates.ServiceBroker.svg)](https://www.nuget.org/packages/Axoom.Templates.ServiceBroker/)
[![Build status](https://img.shields.io/appveyor/ci/AXOOM/templates-servicebroker.svg)](https://ci.appveyor.com/project/AXOOM/templates-servicebroker)

This template helps you implement a Service Broker according to the [Open Service Broker API](https://www.openservicebrokerapi.org/).  
It uses the [OpenServiceBroker.Server library](https://github.com/AXOOM/OpenServiceBroker#server-library).

# Using the template

Download and install the [.NET Core SDK](https://www.microsoft.com/net/download) on your machine. You can then install the template by running the following:

    dotnet new --install Axoom.Templates.ServiceBroker::*

To use the template to create a new project:

    dotnet new axoom-servicebroker --name "MyVendor.ServiceBroker" --serviceName myvendor-servicebroker --friendlyName "My Service Broker"--description "my description"
    cd MyVendor.ServiceBroker

In the commands above replace
- `MyVendor.ServiceBroker` with the .NET namespace you wish to use,
- `myvendor-servicebroker` with the name of your company and service broker using only lowercase letters and hyphens,
- `My Service Broker` with the full name of your service broker and
- `my description` with a brief (single sentence) description of the service broker.

You can now open the generated project using your favorite IDE. We recommend [Visual Studio](https://www.visualstudio.com/downloads/), [Visual Studio Code](https://code.visualstudio.com/Download) or [Rider](https://www.jetbrains.com/rider/).
