using System;


namespace AppWeatherEventNotifier.Services.Events
{
	class EventMessage
    {
		public static event EventHandler EventMessage1;

        public static void RunEvents(object sender, EventArgs e)
        {
            EventMessage1?.Invoke(sender, e);
        }
        public static void ClearAll()
		{
			try
			{
				foreach (Delegate d in EventMessage1.GetInvocationList())
				{
                    EventMessage1 -= (EventHandler)d;
				}
			}
			catch
			{
			}
		}

		
	}
}
