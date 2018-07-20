using QuickLaunchManager.Config;
using QuickLaunchManager.Handlers;
using QuickLaunchManager.Models;
using QuickLaunchManager.Repo;
using QuickLaunchManager.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickLaunchManager
{
    public class QuickLaunchApi
    {
        private readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IRepo _repo;
        private readonly IItemValidator _validator;
        private readonly IDictionary<string, BaseHandler> _handlers;
        public QuickLaunchApi(
            IRepo repo,
            IItemValidator validator,
            BaseHandler[] handlers,
            QuickLaunchAppConfig config)
        {
            try
            {
                logger.Debug("Starting");
                _repo = repo;
                _validator = validator;
                _handlers = handlers.ToDictionary(h => h.HandlerKey);
            }
            catch (Exception x)
            {
                logger.Error(x, "Contructor");
            }
            finally
            {
                logger.Debug("Complete");
            }
        }
        public IEnumerable<HandlerInfo> GetHandlerInfo()
        {
            return _handlers.Keys.Select(GetHandlerInfo);
        }
        public HandlerInfo GetHandlerInfo(string handlerKey)
        {
            var handler = _handlers[handlerKey];
            return new HandlerInfo
            {
                HandlerKey = handlerKey,
                HandlerName = handler.GetType().Name,
                HandlerIcon = handler.handlerIcon
            };
        }
        public IEnumerable<string> GetHandlerKeys()
        {
            return _handlers.Keys.ToList();
        }
        public IEnumerable<QuickLaunchItem> GetAllItems()
        {
            return _repo.GetItems();
        }
        public void AddItem(QuickLaunchItem item)
        {
            _repo.AddItem(item);
        }
        public OperationResult Validate(QuickLaunchItem item)
        {
            var handler = item?.Handler;
            if(handler==null || !_handlers.Keys.Contains(handler))
            {
                return new OperationResult(
                    nameof(item.Handler),
                    Severity.Error,
                    "Unknown handler specified");
            }
            return _handlers[item.Handler].Validate(item);
        }
        public void Open(Guid id) => Handle(_repo.GetItem(id));
        public bool Handle(QuickLaunchItem item)
        {
            if (_handlers.TryGetValue(item.Handler, out var handler))
            {
                return handler.Handle(item);
            }
            throw new Exception($"Could not find handler for type '{item.Handler}'");
        }
        public void UpdateOrCreate(IEnumerable<QuickLaunchItem> items)
        {
            foreach (var item in items)
            {
                if (item.Id == null)
                {
                    _repo.AddItem(item);
                }
                else
                {
                    _repo.UpdateItem(item);
                }
            }
            _repo.Save();
        }

    }
}
