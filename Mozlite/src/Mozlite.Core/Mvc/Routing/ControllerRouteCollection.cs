using System.Collections;
using System.Collections.Generic;

namespace Mozlite.Mvc.Routing
{
    /// <summary>
    /// ������·�ɼ��ϡ�
    /// </summary>
    public class ControllerRouteCollection : IEnumerable<ControllerRoute>
    {
        private readonly IList<ControllerRoute> _routes = new List<ControllerRoute>();

        /// <inheritdoc />
        public IEnumerator<ControllerRoute> GetEnumerator()
        {
            return _routes.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// ���·�ɡ�
        /// </summary>
        /// <param name="type">·�����͡�</param>
        /// <param name="controllerName">���������ơ�</param>
        /// <param name="area">�������ơ�</param>
        public void AddRoute(RouteType type, string controllerName, string area)
        {
            var route = new ControllerRoute();
            route.ControllerName = controllerName.Substring(0, controllerName.Length - 10);
            var routeName = route.ControllerName.ToLower();
            if (type == RouteType.User && routeName.StartsWith("user"))
                route.RouteName = routeName.Substring(4);
            else if (type == RouteType.Backend && routeName.StartsWith("admin"))
                route.RouteName = routeName.Substring(5);
            else
                route.RouteName = routeName;
            route.Type = type;
            route.Area = area;
            _routes.Add(route);
        }
    }
}