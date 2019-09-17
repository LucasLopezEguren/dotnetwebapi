using System;
using System.Linq;
using System.Collections.Generic;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.DataAccess;
using CompanyDashboard.BusinessLogic.Exceptions;


namespace CompanyDashboard.BusinessLogic
{
    public class IndicatorLogic : IIndicatorLogic
    {
        private IRepository<Indicator> repository;
        public IndicatorLogic(IRepository<Indicator> repository)
        {
            if (repository == null)
            {
                this.repository = new IndicatorRepository(ContextFactory.GetNewContext());
            }
            else
            {
                this.repository = repository;
            }
        }

        public Indicator AddIndicator(Indicator indicator)
        {
            if (repository.Has(indicator)) throw new AlreadyExistsException("Indicador ya existente");
            repository.Add(indicator);
            repository.Save();
            return indicator;
        }

        public void DeleteIndicator(int id)
        {
            Indicator indicator = new Indicator();
            indicator.ID=id;
            if (!repository.Has(indicator)) throw new DoesNotExistsException("Indicador no existe");
            UserIndicatorRepository userIndicatorRepository = new UserIndicatorRepository();
            indicator = repository.GetByID(id);
            List<UserIndicator> userIndicators = userIndicatorRepository.GetAll().ToList();
            foreach(UserIndicator userIndicator in userIndicators){
                if(userIndicator.indicator == id){
                    userIndicatorRepository.Delete(userIndicator);
                    userIndicatorRepository.Save();
                }
            }
            NodeRepository nr = new NodeRepository();
            if(!(repository.GetByID(id).Green == null)){
                nr.Delete(repository.GetByID(id).Green);
            }
            if(!(repository.GetByID(id).Red == null)){
                nr.Delete(repository.GetByID(id).Red);
            }
            if(!(repository.GetByID(id).Yellow == null)){
                nr.Delete(repository.GetByID(id).Yellow);
            }
            nr.Save();
            repository.Delete(indicator);
            repository.Save();
        }

        public void UpdateIndicator(Indicator indicator)
        {
            if (!repository.Has(indicator)) throw new DoesNotExistsException("Indicador no existe");
            repository.Update(indicator);
            repository.Save();
        }


        public IEnumerable<Indicator> GetAllIndicators()
        {
            IEnumerable<Indicator> toReturn = repository.GetAll();
            return toReturn;
        }

        public IEnumerable<Indicator> GetAllRedIndicators(IEnumerable<Indicator> indicators)
        {
            List<Indicator> toReturn = new List<Indicator>();
            NodeLogic nodeLogic = new NodeLogic(null);
            foreach (Indicator indicator in indicators){
                if(nodeLogic.Evaluate(indicator.Red))
                toReturn.Add(indicator);
            }
            return toReturn;
        }

        public IEnumerable<Indicator> GetAllYellowIndicators(IEnumerable<Indicator> indicators)
        {
            List<Indicator> toReturn = new List<Indicator>();
            NodeLogic nodeLogic = new NodeLogic(null);
            foreach (Indicator indicator in indicators){
                if(nodeLogic.Evaluate(indicator.Yellow))
                toReturn.Add(indicator);
            }
            return toReturn;
        }

        public IEnumerable<Indicator> GetAllGreenIndicators(IEnumerable<Indicator> indicators)
        {
            List<Indicator> toReturn = new List<Indicator>();
            NodeLogic nodeLogic = new NodeLogic(null);
            foreach (Indicator indicator in indicators){
                if(nodeLogic.Evaluate(indicator.Green))
                toReturn.Add(indicator);
            }
            return toReturn;
        }

        public Indicator GetById (int id){
            return repository.GetByID(id);
        }
    }
}
