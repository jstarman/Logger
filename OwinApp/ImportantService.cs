using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinApp
{
    public class ImportantService : BaseService
    {
        private ILog _logger;
        public ImportantService(ILog logger)
        {
            _logger = logger;
        }

        public void DoSomethingImportant() 
        {
            try
            {
                Run(new ImportantServiceException("The important exception message"));
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to do anything important", ex);
            }
        }
    }

    public class ImportantServiceException : Exception 
    {
        public ImportantServiceException(string msg): base(msg)
        {

        }
    }


    public class LongRunningProcess : BaseService
    {
        private ILog _logger;
        public LongRunningProcess(ILog logger)
        {
            _logger = logger;
        }

        public void StartProcess()
        {
            try
            {
                Run(new LongRunningProcessException("The process exception message"));
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to do process", ex);
            }
        }
    }

    public class LongRunningProcessException : Exception
    {
        public LongRunningProcessException(string msg)
            : base(msg)
        {

        }
    }

    public class WebRequestClient : BaseService
    {
        private ILog _logger;
        public WebRequestClient(ILog logger)
        {
            _logger = logger;
        }

        public void StartRequest()
        {
            try
            {
                Run(new WebRequestClientException("The request exception message"));
            }
            catch (Exception ex)
            {
                _logger.Error("Failed to request", ex);
            }
        }
    }

    public class WebRequestClientException : Exception
    {
        public WebRequestClientException(string msg)
            : base(msg)
        {
        }
    }

    public class BaseService
    {        
        public BaseService()
        {

            _internalService = new InternalService();
        }

        private  InternalService _internalService;


        public void Run(Exception e)
        {
            _internalService.StartThrowingStuff(e);
        }
    }

    public class InternalService 
    {
        public void StartThrowingStuff(Exception e)
        {
            throw e;
        }
    }
}
