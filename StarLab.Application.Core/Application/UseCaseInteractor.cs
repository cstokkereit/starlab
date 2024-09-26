using AutoMapper;

namespace StarLab.Application
{
    public class UseCaseInteractor<T>
    {
        private readonly IMapper mapper;

        private readonly T outputPort;

        public UseCaseInteractor(T outputPort, IMapper mapper)
        {
            this.outputPort = outputPort;
            this.mapper = mapper;
        }

        protected IMapper Mapper => mapper;

        protected T OutputPort => outputPort;
    }
}
