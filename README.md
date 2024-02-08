
# sms.net.bd NuGet Package Release Note
[![Static Badge](https://img.shields.io/badge/NuGet-1.1.1-blue?style=flat)
](https://www.nuget.org/packages/smsnetbd.Csharp.Client)
![Static Badge](https://img.shields.io/badge/.Net_Core-6.0-purple?style=flat)

### Summary:
The sms.net.bd NuGet package provides a simple interface to send SMS messages using the sms.net.bd API. This release introduces initial support for sending SMS messages, scheduling SMS messages, checking SMS delivery reports, and retrieving account balances.

### Initialization:
To start using the sms.net.bd NuGet package, follow these steps:

1. **Install the Package**: Install the sms.net.bd NuGet package in your project using the following command in the NuGet Package Manager Console:

   ```shell
   NuGet\Install-Package smsnetbd.Csharp.Client
   ```

2. **Initialize SMS Client**: Create an instance of the `SMS` class by providing your API key. This API key can be obtained from the [sms.net.bd API website](https://www.sms.net.bd/api).

   ```csharp
   using sms.net.bd;

   // Initialize SMS client with your API key
   SMS smsClient = new SMS("Your-API-Key");
   ```

### Usage:
After initializing the SMS client, you can use its methods to interact with the sms.net.bd API. Below are the available methods and their usage:

1. **SendSMS**: Send a text message to a specified phone number.

   ```csharp
   // Send SMS message
   string phoneNumber = "01800000000";
   string message = "Hello, world!";
   string sender_id = "xxxxxxx"  //Optional. If you have an approved Sender ID. 
   string response = await smsClient.SendSMS(phoneNumber, message, sender_id);
   ```
   > Response
    ```
    {
      "error": 0,
      "msg": "Request successfully submitted",
      "data": {
        "request_id": 0000
      }
    }
2. **ScheduleSMS**: Schedule a text message to be sent at a specified time.

   ```csharp
   // Schedule SMS message
   string phoneNumber = "01800000000";
   string message = "Hello, world!";
   string scheduleTime = "2023-11-01 12:00:00"; // Specify the scheduled time in ISO 8601 format
   string response = await smsClient.ScheduleSMS(phoneNumber, message, scheduleTime);
   ```
   > Response

    ```
    {
      "error": 0,
      "msg": "Request successfully submitted"
    }
3. **GetReport**: Retrieve the delivery report of an SMS message.

   ```csharp
   // Get SMS delivery report
   string messageId = 12345; // Specify the ID of the SMS message
   string report = await smsClient.GetReport(messageId);
   ```
   > Response
	```
	{
	  "error":0,
	  "msg":"Success",
	  "data":{"request_id":000000,
		 "request_status":"Complete",
		 "request_charge":"0.0000",
		 "recipients":[
		  {
			"number":"01800000000",
			"charge":"0.0000",
			"status":"Sent"
		  }
		]
	  }
	}
	```
4. **GetBalance**: Retrieve the current account balance.

   ```csharp
   // Get account balance
   string balance = await smsClient.GetBalance();
   ```
	> Response

	    {
	      "error": 0,
	      "msg": "Success",
	      "data": {
	        "balance": "00.0000"
	      }
	    }


### Error Codes:

| Common Errors |  |
|--|--|
| Error - 0 | Success. Everything worked as expected. |
| Error - 400 | The request was rejected, due to a missing or invalid parameter. |
| Error - 403 | You don't have permissions to perform the request. |
| Error - 404 | The requested resource not found. |
| Error - 405 | Authorization required. |
| Error - 409 | Unknown error occurred on Server end. |


| Send SMS Errors |  |
|--|--|
| Error - 410 | Account expired. |
| Error - 411 | Reseller Account expired or suspended. |
| Error - 412 | Invalid Schedule. |
| Error - 413 | Invalid Sender ID. |
| Error - 414 | Message is empty. |
| Error - 415 | Message is too long. |
| Error - 416 | No valid number found. |
| Error - 417 | Insufficient balance. |
| Error - 418 | Content Blocked. |

### Feedback and Support

We welcome your feedback and suggestions for improving the sms.net.bd NuGet package. If you encounter any issues or have questions, please contact [Your Contact Information] or open an issue on the GitHub repository.

Thank you for using sms.net.bd!


For more details on the sms.net.bd API and its usage, refer to the [official API documentation](https://www.sms.net.bd/api).
