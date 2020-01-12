using System;
using System.Collections.Generic;
using System.Text;

namespace AzureQueueLibrary.Infrastructure
{
	public class QueueConfig
	{
		public string QueueConnectionString { get; set; }

		public QueueConfig()
		{

		}

		public QueueConfig(string queueConnectionString)
		{
			QueueConnectionString = queueConnectionString;
		}
	}
}
