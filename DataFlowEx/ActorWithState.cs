using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FrameworkEx
{
    public class ActorWithState<TInputMessage, TState, TReplyMessage> : IDisposable, ITargetBlock<TInputMessage> where TInputMessage : class
    {
        private readonly ActionBlock<TInputMessage> _msgQueue;
        private TState _state;
        private readonly Func<TInputMessage, TState, TState> _process;
        private readonly Action<Exception> _errorHandler;

        public ActorWithState(TState state, Func<TInputMessage, TState, TState> process, Func<TState,TReplyMessage> extractReply,
            Action<TReplyMessage> reply, Func<TState, TState> stateClear, Action<Exception> errorHandler)
        {
            _state = state;
            _process = process;
            _errorHandler = errorHandler;
            _msgQueue = new ActionBlock<TInputMessage>
            (
                x =>
                    {
                        var newState = Process(x);
                        var replyMessage = extractReply(newState);
                        _state = stateClear(newState);
                        reply(replyMessage);
                    }
            );
        }

        public void Post(TInputMessage message)
        {
            _msgQueue.Post(message);
        }

        private TState Process(TInputMessage msg)
        {
            try
            {
                return _process(msg, _state);
            }
            catch (Exception ex)
            {
                _errorHandler(ex);
            }

            return _state;
        }

        public void Dispose()
        {
            _msgQueue.Complete();
        }

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInputMessage messageValue, ISourceBlock<TInputMessage> source,
                                                  bool consumeToAccept)
        {
            return ((ITargetBlock<TInputMessage>) _msgQueue).OfferMessage(messageHeader, messageValue, source,consumeToAccept);
        }

        public void Complete()
        {
            _msgQueue.Complete();
        }

        public void Fault(Exception exception)
        {
            ((ITargetBlock<TInputMessage>)_msgQueue).Fault(exception);
        }

        public Task Completion 
        {
            get { return _msgQueue.Completion; }
        }
    }
}