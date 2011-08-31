﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MyToolkit.Messages
{
	class MyTriple
	{
		public object Receiver;
		public Type Type;
		public object Action; 
	}

	public static class Messenger
	{
		private static readonly List<MyTriple> actions = new List<MyTriple>();
		
		/// <summary>
		/// Registers an action for the given receiver.  
		/// </summary>
		/// <typeparam name="T">Type of the message</typeparam>
		/// <param name="receiver">Receiver to use as identifier</param>
		/// <param name="action">Action to register</param>
		public static void Register<T>(object receiver, Action<T> action)
		{
			actions.Add(new MyTriple { Receiver = receiver, Type = typeof(T), Action = action});
		}

		/// <summary>
		/// Registers an action for no receiver. 
		/// </summary>
		/// <typeparam name="T">Type of the message</typeparam>
		/// <param name="action">Action to register</param>
		public static void Register<T>(Action<T> action)
		{
			Register(null, action);
		}

		/// <summary>
		/// Unregisters all actions with no receiver
		/// </summary>
		public static void Unregister()
		{
			Unregister(null);
		}

		/// <summary>
		/// Unregisters all actions with the given receiver
		/// </summary>
		/// <param name="receiver"></param>
		public static void Unregister(object receiver)
		{
			foreach (var a in actions.Where(a => a.Receiver == receiver).ToArray())
				actions.Remove(a);
		}

		/// <summary>
		/// Unregisters the specified action
		/// </summary>
		/// <typeparam name="T">Type of the message</typeparam>
		/// <param name="action">Action to unregister</param>
		public static void Unregister<T>(Action<T> action)
		{
			foreach (var a in actions.Where(a => (Action<T>)a.Action == action).ToArray())
				actions.Remove(a);
		}

		/// <summary>
		/// Unregisters the specified action
		/// </summary>
		/// <typeparam name="T">Type of the message</typeparam>
		public static void Unregister<T>()
		{
			foreach (var a in actions.Where(a => a.Type == typeof(T)).ToArray())
				actions.Remove(a);
		}

		/// <summary>
		/// Unregisters the specified action
		/// </summary>
		/// <param name="receiver"></param>
		/// <typeparam name="T">Type of the message</typeparam>
		public static void Unregister<T>(object receiver)
		{
			foreach (var a in actions.Where(a => a.Receiver == receiver && a.Type == typeof(T)).ToArray())
				actions.Remove(a);
		}

		/// <summary>
		/// Unregisters an action for the specified receiver. 
		/// </summary>
		/// <typeparam name="T">Type of the message</typeparam>
		/// <param name="receiver"></param>
		/// <param name="action"></param>
		public static void Unregister<T>(object receiver, Action<T> action)
		{
			foreach (var a in actions.Where(a => a.Receiver == receiver && (Action<T>)a.Action == action).ToArray())
				actions.Remove(a);
		}

		/// <summary>
		/// Sends a message to the registered receivers. 
		/// </summary>
		/// <typeparam name="T">Type of the message</typeparam>
		/// <param name="message"></param>
		public static void Send<T>(T message)
		{
			var type = typeof (T);
			foreach (var a in actions.Where(a => a.Type == type).ToArray())
				((Action<T>)a.Action)(message);
		}
	}
}
