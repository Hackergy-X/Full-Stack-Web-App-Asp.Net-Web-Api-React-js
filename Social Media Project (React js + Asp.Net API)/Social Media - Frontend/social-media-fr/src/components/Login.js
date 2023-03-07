import React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

function Login() {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleSave = async (e) => {
    e.preventDefault();
    if(email === "" || password === ""){
      alert("Please fill all the fields");
      return;
    }else{
      const url = "https://localhost:7226/api/Registration/Login";
    const data = {
      Name: "string",
      Email: email,
      Password: password,
      PhoneNo: "string",
    };

    await axios
      .post(url, data)
      .then((response) => {
        clear();
        const dt = response.data;
        if(dt.statusCode === 200){
          if(email === "admin" && password === "admin"){
            localStorage.setItem("username", email);
            navigate("/admindashboard");
          }else{
            localStorage.setItem("loggedEmail", email);
            localStorage.setItem("username", dt.registration.name);
            navigate("/userdashboard");
            // if(dt.registration.userType === "Customer"){
            //   history.push("/staffcustomerdashboard");
            // }else{
            //   history.push("/userdashboard");
            // }
          }
        }
      })
      .catch((error) => {
        console.log(error.response);
      });
    }
  };

  const clear = () => {
    setEmail("");
    setPassword("");
  };

  const goToRegister = (e) => {
    e.preventDefault();
    window.location.href = "/register";
  };

  return (
    <div>
      <section className="vh-100">
        <div className="container py-5 h-100">
          <div className="row d-flex align-items-center justify-content-center h-100">
            <div className="col-md-8 col-lg-7 col-xl-6">
              <img
                src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.svg"
                className="img-fluid"
                alt="Phone image"
              />
            </div>
            <div className="col-md-7 col-lg-5 col-xl-5 offset-xl-1">
              <h3 className="mb-5 text-uppercase">Login</h3>
              <form>
                <div className="form-outline mb-4">
                  <label
                    className="form-label"
                    htmlFor="form1Example13"
                    style={{
                      width: "100%",
                      textAlign: "left",
                      fontSize: "1.2rem",
                      fontWeight: "300",
                    }}
                  >
                    Email
                  </label>
                  <input
                    type="email"
                    id="form1Example13"
                    className="form-control form-control-lg"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                  />
                </div>

                <div className="form-outline mb-4">
                  <label
                    className="form-label"
                    htmlFor="form1Example23"
                    style={{
                      width: "100%",
                      textAlign: "left",
                      fontSize: "1.2rem",
                      fontWeight: "300",
                    }}
                  >
                    Password
                  </label>
                  <input
                    type="password"
                    id="form1Example23"
                    className="form-control form-control-lg"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  />
                </div>
                <a href="/register" className="text-body">
                  <p className="small mb-5 pb-lg-2">
                    Don't have an account? Register
                  </p>
                </a>
                <button
                  type="submit"
                  className="btn btn-primary btn-lg btn-block"
                  onClick={(e) => {
                    handleSave(e);
                  }}
                  style={{
                    width: "100%",
                  }}
                >
                  Login
                </button>

                <button
                  type="submit"
                  className="btn btn-primary btn-lg btn-block mt-1"
                  onClick={(e) => {
                    goToRegister(e);
                  }}
                  style={{
                    "width": "100%",
                    "background-color":"#FFC107",
                    "border-color":"#FFC107",
                    "color":"#000"
                  }}
                >
                  Register
                </button>

                <div className="divider d-flex align-items-center my-4">
                  <p className="text-center fw-bold mx-3 mb-0 text-muted">OR</p>
                </div>

                <a
                  className="btn btn-primary btn-lg btn-block mb-1"
                  style={{
                    backgroundColor: "#3b5998",
                    width: "100%",
                  }}
                  href="#!"
                  role="button"
                >
                  <i className="fab fa-facebook-f me-2"></i>Continue with
                  Facebook
                </a>
                <a
                  className="btn btn-primary btn-lg btn-block"
                  style={{
                    backgroundColor: "#55acee",
                    width: "100%",
                  }}
                  href="#!"
                  role="button"
                >
                  <i className="fab fa-twitter me-2"></i>Continue with Twitter
                </a>
              </form>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}

export default Login;
