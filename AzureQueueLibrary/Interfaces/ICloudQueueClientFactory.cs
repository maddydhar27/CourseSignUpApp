using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureQueueLibrary.Interfaces
{    public interface ICloudQueueClientFactory
    {
        CloudQueueClient GetClient();
    }
}
