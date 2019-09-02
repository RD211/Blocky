void setup()
{
    Serial.begin(9600);
}
bool isFull(int arr[])
{
    int a = arr[0];
    for(int i = 1; i<500; i++)
    {
        if(abs(a-arr[i])>3)
        {
            return false;
        }
    }
    return true;
}
void loop()
{
    if(Serial.available()>0)
    {
        char x = Serial.read();
        if(x == '!')
        {
            Serial.print("Detected");
        }
        else if(x=='*')
        {
            String a = "";
            int arr[500];
            int order [] ={7,3,5,6,4,2,1,0};
            for(int i = 0; i<8; i++)
            {
                for(int j = 0; j<500; j++)
                {
                    arr[j] = analogRead(order[i]);
                }
                if(isFull(arr))
                {
                  //ahead
                  if(arr[0]>=830&& arr[0]<=850){
                    arr[0] = 840; 
                  }
                  //
                  //if ahead
                  if(arr[0]>=590&&arr[0]<=620){
                    arr[0]=600;
                  }
                  if(arr[0]>=720&&arr[0]<=740)
                  {
                    arr[0]=730;
                  }
                  //if right
                  if(arr[0]>=675&& arr[0]<=690)
                  {
                    arr[0]= 680;
                  }
                  //if left
                  if(arr[0]>=360&& arr[0]<=380)
                  {
                    arr[0]= 370;
                  }
                  //while ahead
                  if(arr[0]>=490&& arr[0]<=510)
                  {
                    arr[0]= 500;
                  }
                  //while right
                  if(arr[0]>=540&& arr[0]<=560)
                  {
                    arr[0]= 550;
                  }
                  //while left
                  if(arr[0]>=310&& arr[0]<=340)
                  {
                    arr[0]= 320;
                  }
                  //end
                  if(arr[0]>=691&& arr[0]<=710)
                  {
                    arr[0]= 700;
                  }
                  //turn left
                  if(arr[0]>=290 && arr[0]<=310)
                  {
                    arr[0]= 300;
                  }
                  if(arr[0]>=1020)
                  {
                    arr[0]= 730;
                  }
                  if(arr[0]>=648&&arr[0]<=670){
                    arr[0]=660; 
                  }
                  arr[0]-=arr[0]%10;
                    a = a + arr[0]+'.';
                }
            }
            //a="730.600.840.400.300.700.700.";
            Serial.println(a);
            a="";
        }
    }
}
