# sms.net.bd NuGet Package Documentation

[![NuGet Version](https://img.shields.io/badge/NuGet-2.0.0-blue?style=flat)](https://www.nuget.org/packages/smsnetbd.Csharp.Client)
![.NET Standard](https://img.shields.io/badge/.NET_Standard-2.0-purple?style=flat)
![.NET Framework](https://img.shields.io/badge/.NET_Framework-4.6.1+-critical?style=flat)
![Xamarin.iOS](https://img.shields.io/badge/Xamarin.iOS-10.14+-lightgrey?style=flat)
![Xamarin.Android](https://img.shields.io/badge/Xamarin.Android-8.0+-success?style=flat)

## Overview

The `smsnetbd.Csharp.Client` package provides a simple and powerful interface to interact with the sms.net.bd API for sending SMS messages, managing campaigns, and monitoring account status.

## Installation

Install the package via NuGet Package Manager Console:
```shell
NuGet\Install-Package smsnetbd.Csharp.Client
```

## Getting Started

### Initialize the SMS Client

Obtain your API key from [sms.net.bd API portal](https://www.sms.net.bd/api) and initialize the client:
```csharp
using sms.net.bd;

// Initialize SMS client with your API key
SMS smsClient = new SMS("your-api-key");
```

## Features & Usage

### 1. Send SMS

Send a text message to a specified phone number.
```csharp
string phoneNumber = "01800000000";
string message = "Hello, world!";
// string senderId = "xxxxxxx";  // Optional: Use if you have an approved Sender ID

string response = await smsClient.SendSMS(phoneNumber, message, senderId);
```

**Response:**
```json
{
  "error": 0,
  "msg": "Request successfully submitted",
  "data": {
    "request_id": 12345
  }
}
```

### 2. Schedule SMS

Schedule a text message to be sent at a specified future time.
```csharp
string phoneNumber = "01800000000";
string message = "Hello, world!";
string scheduleTime = "2023-11-01 12:00:00"; // Format: YYYY-MM-DD HH:MM:SS
// string senderId = "xxxxxxx";  // Optional: Use if you have an approved Sender ID

string response = await smsClient.ScheduleSMS(phoneNumber, message, scheduleTime, senderId);
```

**Response:**
```json
{
  "error": 0,
  "msg": "Request successfully submitted"
}
```

### 3. Get SMS Delivery Report

Retrieve the delivery status of a sent SMS message.
```csharp
int requestId = 12345; // Request ID from SendSMS response

string report = await smsClient.GetReport(requestId);
```

**Response:**
```json
{
  "error": 0,
  "msg": "Success",
  "data": {
    "request_id": 12345,
    "request_status": "Complete",
    "request_charge": "0.2500",
    "recipients": [
      {
        "number": "01800000000",
        "charge": "0.2500",
        "status": "Sent"
      }
    ]
  }
}
```

### 4. Get Account Balance

Check your current SMS account balance.
```csharp
string balance = await smsClient.GetBalance();
```

**Response:**
```json
{
  "error": 0,
  "msg": "Success",
  "data": {
    "balance": "150.5000"
  }
}
```

### 5. Get All Approved Campaign Content (New in v2.0.0)

Retrieve a list of all approved campaign content from your account.
```csharp
string campaigns = await smsClient.GetCampaigns();
Console.WriteLine(campaigns);
```

### 6. Send Campaign SMS (New in v2.0.0)

Send an SMS using pre-approved campaign content by content ID.
```csharp
string phoneNumber = "01800000000";
string contentId = "XXXXX"; // ID of approved campaign content

string response = await smsClient.CampaignSMS(phoneNumber, contentId);
Console.WriteLine(response);
```

## Error Codes Reference

### Common Errors

| Error Code | Description |
|------------|-------------|
| 0 | Success - Everything worked as expected |
| 400 | Bad Request - Missing or invalid parameter |
| 403 | Forbidden - Insufficient permissions |
| 404 | Not Found - Requested resource doesn't exist |
| 405 | Unauthorized - Authorization required |
| 409 | Server Error - Unknown error on server |

### SMS-Specific Errors

| Error Code | Description |
|------------|-------------|
| 410 | Account expired |
| 411 | Reseller account expired or suspended |
| 412 | Invalid schedule time format |
| 413 | Invalid or unapproved Sender ID |
| 414 | Message content is empty |
| 415 | Message exceeds maximum length |
| 416 | No valid recipient number found |
| 417 | Insufficient account balance |
| 418 | Content blocked by filter |

## Platform Compatibility

The package supports .NET Standard 2.0, ensuring compatibility with:

- .NET Framework 4.6.1+
- .NET Core 2.0+
- .NET 5, 6, 7, 8+
- Xamarin.iOS 10.14+
- Xamarin.Android 8.0+
- Mono, UWP, and more

## Resources

- **API Documentation**: [https://www.sms.net.bd/api](https://www.sms.net.bd/api)
- **NuGet Package**: [https://www.nuget.org/packages/smsnetbd.Csharp.Client](https://www.nuget.org/packages/smsnetbd.Csharp.Client)
- **GitHub Repository**: [https://github.com/smsnetbd/sms-net-bd-csharp-nuget](https://github.com/smsnetbd/sms-net-bd-csharp-nuget)

## Support & Feedback

We value your feedback! If you encounter any issues or have suggestions:

- Contact our support team
- [Open an issue on GitHub](https://github.com/smsnetbd/sms-net-bd-csharp-nuget/issues)
- Star us on GitHub if you find this package helpful!

**Thank you for using sms.net.bd!**
