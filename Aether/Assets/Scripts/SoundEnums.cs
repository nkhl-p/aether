public enum SoundEnums {
    THEME,
    COLLISION,
    JUMP,
    FALL,
    WIN,
    POWERUP,
    YELLOW_LOSE,
    LASER_SHOOT,
    TARGET_MISS_LASER
}

public static class ErrorLevelExtensions {
    public static string GetString(this SoundEnums me) {
        switch (me) {
            case SoundEnums.THEME:
                return "SpaceTravel";
            case SoundEnums.COLLISION:
                return "Collision";
            case SoundEnums.JUMP:
                return "Jump";
            case SoundEnums.FALL:
                return "Fall";
            case SoundEnums.WIN:
                return "Win";
            case SoundEnums.POWERUP:
                return "PowerUp";
            case SoundEnums.YELLOW_LOSE:
                return "YellowLose";
            case SoundEnums.LASER_SHOOT:
                return "LaserShoot";
            case SoundEnums.TARGET_MISS_LASER:
                return "TargetMissLaser";
            default:
                return "";
        }
    }
}
