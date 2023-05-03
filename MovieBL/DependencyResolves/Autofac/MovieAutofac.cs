using Autofac;
using MovieBL.Abstracts;
using MovieBL.Concretes;
using MovieDataAccess.Abstracts;
using MovieDataAccess.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBL.DependencyResolves.Autofac
{
    public class MovieAutofac:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MovieRepository>().As<IMovieRepository>();
            builder.RegisterType<MovieService>().As<IMovieService>();
        }
    }
}
