using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBooking.Functionality.Contexts;
using TicketBooking.Functionality.Repositories;
using TicketBooking.Functionality.Services;
using TicketBooking.Functionality.UnitOfWorks;

namespace TicketBooking.Functionality
{
    public class FunctionalityModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public FunctionalityModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TicketBookingContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<TicketBookingContext>().As<ITicketBookingContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();

            builder.RegisterType<TicketRepository>().As<ITicketRepository>().InstancePerLifetimeScope();

            builder.RegisterType<TicketBookingSystemUnitOfWork>().As<ITicketBookingSystemUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<CustomerService>().As<ICustomerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TicketService>().As<ITicketService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
