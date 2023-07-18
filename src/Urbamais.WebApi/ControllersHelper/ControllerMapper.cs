using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Urbamais.WebApi.ControllersHelper;

public static class ControllerMapper
{
    public static void MapControllers(ApplicationPartManager applicationPartManager)
    {
        // Obtém todos os recursos de partes do aplicativo que contêm controllers
        var feature = new ControllerFeature();
        applicationPartManager.PopulateFeature(feature);

        // Itera sobre cada controller encontrado
        foreach (var controllerType in feature.Controllers)
        {
            // Use o controllerType para realizar qualquer ação desejada,
            // como registrar o controller em algum container de injeção de dependência
            // ou adicionar rotas personalizadas.

            ListControllers.Instance.List.Add(controllerType.Name.Replace("Controller", ""));
        }
    }
}
