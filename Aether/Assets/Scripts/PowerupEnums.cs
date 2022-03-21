public enum PowerupEnums {
    TIME,
    PERMEATE,
    LEVITATE,
    SIZE,
    NONE
}

public static class PowerupExtensions {
    public static string GetString(this PowerupEnums me) {
        switch (me) {
            case PowerupEnums.TIME:
                return "Time";
            case PowerupEnums.PERMEATE:
                return "Permeate";
            case PowerupEnums.LEVITATE:
                return "Levitate";
            case PowerupEnums.SIZE:
                return "Size";
            case PowerupEnums.NONE:
                return "";
            default:
                return "";
        }
    }
}
