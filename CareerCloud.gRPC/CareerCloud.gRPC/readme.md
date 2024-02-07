## Setup
# Create .proto file under Protos directory, include syntax,namespace,package message,service sections
# Add ref in project file right click project->select 'Edit Project File'
`
 <ItemGroup>
    <Protobuf Include="Protos\ApplicantEducation.proto" GrpcServices="Server" />
  </ItemGroup>
`
# Implement service file
# Add this service in program.cs
`app.MapGrpcService<ApplicantEducationService>();`


## Test
# Install postman
# Create gRPC request
# Url and Port info from project - Properties - launch Setting.json, Eg:grpc://localhost:5240
# Service definition section in postman, upload .proto files