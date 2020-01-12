using AzureQueueLibrary.Infrastructure;
using AzureQueueLibrary.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureQueueLibrary.QueueConnection
{
	
	public class CloudQueueClientFactory : ICloudQueueClientFactory
	{
		private readonly QueueConfig _queueConfig;
		private CloudQueueClient _cloudQueueClient;

		public CloudQueueClientFactory(QueueConfig queueConfig)
		{
			_queueConfig = queueConfig;
		}

		public CloudQueueClient GetClient()
		{
			if (_cloudQueueClient != null)
				return _cloudQueueClient;

			var storageAccount = CloudStorageAccount.Parse(_queueConfig.QueueConnectionString);
			_cloudQueueClient = storageAccount.CreateCloudQueueClient();
            return _cloudQueueClient;
        }
	}
}
