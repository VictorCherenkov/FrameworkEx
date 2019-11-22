using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FrameworkEx
{
    public class Actor : ITargetBlock<Action>
    {
        private readonly Action<string, Exception> _onException;
        private readonly ActionBlock<Action> _block;
        public Actor(Action<string, Exception> onException)
        {
            _onException = onException;
            _block = new ActionBlock<Action>(x => Process(x));
        }

        private void Process(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _onException(string.Format("Error processing message. {0}", ex), ex);
            }
        }

        public void Fault(Exception exception)
        {
            _block.CastTo<IDataflowBlock>().Fault(exception);
        }

        public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, Action messageValue, ISourceBlock<Action> source, bool consumeToAccept)
        {
            return _block.CastTo<ITargetBlock<Action>>().OfferMessage(messageHeader, messageValue, source, consumeToAccept);
        }

        public void Complete()
        {
            _block.Complete();
        }

        public Task Completion
        {
            get { return _block.Completion;}
        }

        public int InputCount
        {
            get
            {
                return _block.InputCount;
            }
        }
    }
}