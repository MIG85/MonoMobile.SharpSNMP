// Default listener adapter
// Copyright (C) 2009-2010 Lex Li.
// 
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

/*
 * Created by SharpDevelop.
 * User: lextm
 * Date: 5/31/2009
 * Time: 12:09 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;

namespace Lextm.SharpSnmpLib.Messaging
{
    /// <summary>
    /// Default listener adapter implementation.
    /// </summary>
    [Obsolete("Manager developers should use Manager* adapters. Agent developers should check out snmpd as reference design.")]
    public class DefaultListenerAdapter : IListenerAdapter
    {
        /// <summary>
        /// Occurs when a <see cref="TrapV1Message" /> is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs<TrapV1Message>> TrapV1Received;

        /// <summary>
        /// Occurs when a <see cref="TrapV2Message"/> is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs<TrapV2Message>> TrapV2Received;

        /// <summary>
        /// Occurs when a <see cref="InformRequestMessage"/> is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs<InformRequestMessage>> InformRequestReceived;

        /// <summary>
        /// Occurs when a <see cref="GetRequestMessage"/> is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs<GetRequestMessage>> GetRequestReceived;

        /// <summary>
        /// Occurs when a SET request is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs<SetRequestMessage>> SetRequestReceived;

        /// <summary>
        /// Occurs when a GET NEXT request is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs<GetNextRequestMessage>> GetNextRequestReceived;

        /// <summary>
        /// Occurs when a GET BULK request is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs<GetBulkRequestMessage>> GetBulkRequestReceived;

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="binding">The binding.</param>
        public void Process(ISnmpMessage message, IPEndPoint sender, ListenerBinding binding)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            
            if (sender == null)
            {
                throw new ArgumentNullException("sender");
            }

            if (binding == null)
            {
                throw new ArgumentNullException("binding");
            }

            switch (message.Pdu.TypeCode)
            {
                case SnmpType.TrapV1Pdu:
                    {
                        EventHandler<MessageReceivedEventArgs<TrapV1Message>> handler = TrapV1Received;
                        if (handler != null)
                        {
                            handler(this, new MessageReceivedEventArgs<TrapV1Message>(sender, (TrapV1Message)message, binding));
                        }

                        break;
                    }

                case SnmpType.TrapV2Pdu:
                    {
                        EventHandler<MessageReceivedEventArgs<TrapV2Message>> handler = TrapV2Received;
                        if (handler != null)
                        {
                            handler(this, new MessageReceivedEventArgs<TrapV2Message>(sender, (TrapV2Message)message, binding));
                        }

                        break;
                    }

                case SnmpType.InformRequestPdu:
                    {
                        InformRequestMessage inform = (InformRequestMessage)message;
                        binding.SendResponse(inform.GenerateResponse(), sender);

                        EventHandler<MessageReceivedEventArgs<InformRequestMessage>> handler = InformRequestReceived;
                        if (handler != null)
                        {
                            handler(this, new MessageReceivedEventArgs<InformRequestMessage>(sender, inform, binding));
                        }

                        break;
                    }

                case SnmpType.GetRequestPdu:
                    {
                        EventHandler<MessageReceivedEventArgs<GetRequestMessage>> handler = GetRequestReceived;
                        if (handler != null)
                        {
                            handler(this, new MessageReceivedEventArgs<GetRequestMessage>(sender, (GetRequestMessage)message, binding));
                        }

                        break;
                    }

                case SnmpType.SetRequestPdu:
                    {
                        EventHandler<MessageReceivedEventArgs<SetRequestMessage>> handler = SetRequestReceived;
                        if (handler != null)
                        {
                            handler(this, new MessageReceivedEventArgs<SetRequestMessage>(sender, (SetRequestMessage)message, binding));
                        }

                        break;
                    }

                case SnmpType.GetNextRequestPdu:
                    {
                        EventHandler<MessageReceivedEventArgs<GetNextRequestMessage>> handler = GetNextRequestReceived;
                        if (handler != null)
                        {
                            handler(this, new MessageReceivedEventArgs<GetNextRequestMessage>(sender, (GetNextRequestMessage)message, binding));
                        }

                        break;
                    }

                case SnmpType.GetBulkRequestPdu:
                    {
                        EventHandler<MessageReceivedEventArgs<GetBulkRequestMessage>> handler = GetBulkRequestReceived;
                        if (handler != null)
                        {
                            handler(this, new MessageReceivedEventArgs<GetBulkRequestMessage>(sender, (GetBulkRequestMessage)message, binding));
                        }

                        break;
                    }

                default:
                    break;
            }
        }
    }
}
