﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ziggle.WebSite.Startup))]
namespace Ziggle.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
