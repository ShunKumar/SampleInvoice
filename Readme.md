- Migration already added - So Just Update your database

- You can get predefined customer and product table data after update your database

- Add your database connection string in invoicemodel project web config

Note:

if you get below mentioned error while running this application please add below mentioned code in invoiceDemo.csproj file

error - Could not find a part of the path 'localpath.....\bin\roslyn\csc.exe'.

<Target Name="CopyRoslynFiles" AfterTargets="AfterBuild" Condition="!$(Disable_CopyWebApplication) And '$(OutDir)' != '$(OutputPath)'">
    <ItemGroup>
      <RoslynFiles Include="$(CscToolPath)\*" />
    </ItemGroup>
    <MakeDir Directories="$(WebProjectOutputDir)\bin\roslyn" />
    <Copy SourceFiles="@(RoslynFiles)" DestinationFolder="$(WebProjectOutputDir)\bin\roslyn" SkipUnchangedFiles="true" Retries="$(CopyRetryCount)" RetryDelayMilliseconds="$(CopyRetryDelayMilliseconds)" />
</Target>

