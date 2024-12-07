# azure-servicebus-app

[![ci](https://github.com/atrakic/azure-servicebus-app/actions/workflows/ci.yaml/badge.svg)](https://github.com/atrakic/azure-servicebus-app/actions/workflows/ci.yaml)
![License](https://img.shields.io/github/license/atrakic/azure-servicebus-app)

> This project demonstrates how to send and receive messages with Azure Service Bus.

## Requirements

- Azure cloud account
- azure client cli
- .NET SDK

## Usage

1) Login to azure console

```bash
$ az login
```

2) Create azure infrastructure

```bash
$ python ./configure.py
```

3) Configure
Update appsettings.json section in `configure.py` file with your Azure Service Bus details.


```bash
$ python ./configure.py
```


4) Start a message receiver

```bash
$ dotnet run --project src/Receiver.Worker/Receiver.Worker.csproj
```

5) Start message submitter

```bash
$ dotnet run --project src/Submitter.ConsoleApp/Submitter.ConsoleApp.csproj
```
