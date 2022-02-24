public enum PowerupEnums {
    TIME,
    PERMEABILITY,
    SIZE,
    NONE
}

public static class PowerupExtensions {
    public static string GetString(this PowerupEnums me) {
        switch (me) {
            case PowerupEnums.TIME:
                return "Time";
            case PowerupEnums.PERMEABILITY:
                return "Permeability";
            case PowerupEnums.SIZE:
                return "Size";
            case PowerupEnums.NONE:
                return "";
            default:
                return "";
        }
    }
}
