using System;

namespace AppWeatherEventNotifier.Services.Events
{
	class EventRefresh
    {
		public static event EventHandler EventRefresh1;

        public static void RunEvents(object sender, EventArgs e)
        {
            EventRefresh1?.Invoke(sender, e);
        }
        public static void ClearAll()
		{
			try
			{
				foreach (Delegate d in EventRefresh1.GetInvocationList())
				{
                    EventRefresh1 -= (EventHandler)d;
				}

			}
			catch 
			{
			}
		}	
	}
}
