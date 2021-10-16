# NOAA_GFS_TEMP_FORECAST
.NET 3.1 Web API for temprature forecast by NOAA GFS 



To run the app, first you need wgrib2 CLI Provided by NOAA to let users easily extract specific values from a forecast GRIB file.<br>
You can find the full documentation https://www.cpc.ncep.noaa.gov/products/wesley/wgrib2/ <br>
Download Wgrib2 CLI: https://easyupload.io/0k4mtp <br>

then you need to change this params in appsettings.json file : <br>
<img src="https://user-images.githubusercontent.com/69496372/137588809-676c9886-2bf7-4528-8f3f-adc8edce52f9.png"/> <br>

<b>wgrib2:appPath</b> :  wgrib cli app path in local decive. <br>
<b>NOAA_GFS_S3:grb2filesDirectory</b> : where to store/find the GRIB files in local device. <br>


The API supports GET request to the path "/forecast/{date}/{lat}/{lon}" for temprature forecasts :  <br>
<img src="https://user-images.githubusercontent.com/69496372/137588634-2a10c7ec-1390-4a15-8459-09e0cd09192c.png"/>

For Example : <br>
/forecast/13-June-2021 03:00/32.109/34.855 <br>
Response : <br>

<img src="https://user-images.githubusercontent.com/69496372/137588717-bc2bdf4a-5ce0-4612-94b5-c95336fade51.png"/>


<hr/>

<h2 align="center">Temprature Forecast Flow</h2> 
<p align="center">
  <img src="https://user-images.githubusercontent.com/69496372/137589315-99e290f2-0998-42bb-b216-b64fd5150d53.png" />
</p>

