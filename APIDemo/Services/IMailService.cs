using System;
namespace APIDemo.Services
{
	public interface IMailService
	{
		void Send(string subject, string message);
	}
}

