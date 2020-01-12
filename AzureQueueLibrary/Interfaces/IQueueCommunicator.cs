using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureQueueLibrary.Messages;

namespace AzureQueueLibrary.Interfaces
{
    public interface IQueueCommunicator
    {
        T Read<T>(string message);
        Task SendAsync<T>(T obj) where T : BaseQueueMessage;
    }
}
