using System;
using System.Collections.Generic;
using System.Text;

namespace AzureQueueLibrary.Interfaces
{
	public interface IMessageSerializer
	{
		T Deserialize<T>(string message);
		string Serialize(object obj);
	}
}
