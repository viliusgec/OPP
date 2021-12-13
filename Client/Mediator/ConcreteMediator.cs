using Client.Composite;
using Client.Observer;
using Client.PictureBoxBuilder;

namespace Client.Mediator
{
    public class ConcreteMediator : IMediator
    {
        private readonly Map.MapBase map;
        private readonly MapBuilder MapBuilder;
        private readonly FormsEditor editor;
        private readonly ServerObserver ServerObserver;
        private readonly Room room;

        public ConcreteMediator(Map.MapBase map, FormsEditor editor, MapBuilder MapBuilder, ServerObserver ServerObserver, Room room)
        {
            this.map = map;
            this.map.SetMediator(this);
            this.editor = editor;
            this.MapBuilder = MapBuilder;
            this.MapBuilder.SetMediator(this);
            this.ServerObserver = ServerObserver;
            this.ServerObserver.SetMediator(this);
            this.room = room;
        }
        public void Notify(string message)
        {
            switch (message)
            {
                case "A":
                    map.CreateMap();
                    break;
                case "B":
                    MapBuilder.AddPictureBoxes(editor.playerPictureBox, editor.enemyPictureBox, editor.control, editor.size); // antras component
                    break;
                case "C":
                    MapBuilder.CreateMap(editor.imageList1, map);
                    break;
                case "D":
                    _ = ServerObserver.SendMap(map, room.GetName()); //trečias component
                    break;
                default: break;
            }
        }
    }
}
