
using Microsoft.Extensions.Logging;

namespace ApiRequester.Logging
{
    public static class LoggingEvents
    {
        public static EventId GENERATE_ITEMS = new EventId(1000);
        public static EventId LIST_ITEMS = new EventId(1001);
        public static EventId GET_ITEM = new EventId(1002);
        public static EventId INSERT_ITEM = new EventId(1003);
        public static EventId UPDATE_ITEM = new EventId(1004);
        public static EventId DELETE_ITEM = new EventId(1005);
        public static EventId GET_ITEMS_NUMBER = new EventId(1006);

        public static EventId GET_ITEM_NOTFOUND = new EventId(4000);
        public static EventId UPDATE_ITEM_NOTFOUND = new EventId(4001);
    }
}
