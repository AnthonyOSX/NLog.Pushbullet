using System;

namespace NLog.Pushbullet {

	public class PushbulletException : Exception {

		public PushbulletException() : base() { }

		public PushbulletException(string message) : base(message) { }

		public PushbulletException(string message, params object[] args) : base(String.Format(message, args)) { }

	}

}
