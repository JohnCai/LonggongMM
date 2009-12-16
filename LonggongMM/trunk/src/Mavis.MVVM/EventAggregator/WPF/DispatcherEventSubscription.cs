//===============================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Windows.Threading;

namespace Mavis.MVVM
{
    ///<summary>
    /// Extends <see cref="EventSubscription{TPayload}.Action"/> to invoke the <see cref="EventSubscription{TPayload}"/> delegate
    /// in a specific <see cref="Action{T}"/>.
    ///</summary>
    /// <typeparam name="TPayload">The type to use for the generic <see cref="System"/> and <see cref="EventSubscription{TPayload}"/> types.</typeparam>
    public class DispatcherEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        private readonly Dispatcher dispatcher;

        ///<summary>
        /// Creates a new instance of <see cref="BackgroundEventSubscription{TPayload}"/>.
        ///</summary>
        ///<param name="actionReference">A reference to a delegate of type <see cref="System.Action{TPayload}"/>.</param>
        ///<param name="filterReference">A reference to a delegate of type <see cref="Predicate{TPayload}"/>.</param>
        ///<param name="dispatcher">The dispatcher to use when executing the <paramref name="actionReference"/> delegate.</param>
        ///<exception cref="ArgumentNullException">When <paramref name="actionReference"/> or <see paramref="filterReference"/> are <see langword="null" />.</exception>
        ///<exception cref="ArgumentException">When the target of <paramref name="actionReference"/> is not of type <see cref="System.Action{TPayload}"/>,
        ///or the target of <paramref name="filterReference"/> is not of type <see cref="Predicate{TPayload}"/>.</exception>
        public DispatcherEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference, Dispatcher dispatcher)
            : base(actionReference, filterReference)
        {
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Invokes the specified <see cref="System.Action{TPayload}"/> asynchronously in the specified <see cref="Dispatcher"/>.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="argument">The payload to pass <paramref name="action"/> while invoking it.</param>
        public override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            dispatcher.BeginInvoke(DispatcherPriority.Normal, action, argument);
        }
    }
}