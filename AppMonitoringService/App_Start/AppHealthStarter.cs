using System;
using AppHealth;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(AppMonitoringService.App_Start.MySuperPackage), "PreStart")]

namespace AppMonitoringService.App_Start {
    public static class MySuperPackage {
        public static void PreStart() {
            ApplicationHealth.sendApplicationHealth();
        }
    }
}