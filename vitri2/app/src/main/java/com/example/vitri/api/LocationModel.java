package com.example.vitri.api;

import com.google.gson.annotations.SerializedName;

public class LocationModel {
    @SerializedName("latitude")
    private double latitude;

    @SerializedName("longitude")
    private double longitude;

    public LocationModel(double latitude, double longitude) {
        this.latitude = latitude;
        this.longitude = longitude;
    }

    // Các getter và setter
    public double getLatitude() {
        return latitude;
    }

    public void setLatitude(double latitude) {
        this.latitude = latitude;
    }

    public double getLongitude() {
        return longitude;
    }

    public void setLongitude(double longitude) {
        this.longitude = longitude;
    }
}
