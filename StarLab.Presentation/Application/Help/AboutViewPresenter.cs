﻿using AutoMapper;
using StarLab.Commands;
using System.Reflection;

namespace StarLab.Application.Help
{
    internal class AboutViewPresenter : ControlViewPresenter<IAboutView>, IAboutViewPresenter
    {
        public AboutViewPresenter(IAboutView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public override string Name => View.Name + Constants.CONTROLLER;

        public override void Initialise(IApplicationController applicationController)
        {
            base.Initialise(applicationController);

            //View.SetCompanyName(Resources.CompanyName);
            //View.SetCopyright(Resources.Copyright);
            //View.SetDescription(Resources.ProductDescription);
            //View.SetLogo("");
            //View.SetProductName(Resources.StarLab);
            //View.SetVersion(string.Format(Resources.Version, GetVersion()));
        }

        private string GetVersion()
        {
            string version = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();

            if (assembly != null)
            {
                AssemblyName name = assembly.GetName();

                if (name != null && name.Version != null)
                {
                    version = name.Version.ToString();
                }
            }

            return version;
        }
    }
}
