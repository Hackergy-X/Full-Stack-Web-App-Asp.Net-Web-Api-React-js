import React, { useState } from "react";
import axios from "axios";

function Registration() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [phoneNo, setPhoneNo] = useState("");

  const handleSave = async () => {
    if (name === "" || email === "" || password === "" || phoneNo === "") {
      alert("Please fill all the fields");
      return;
    } else {
      const url = "https://localhost:7226/api/Registration/Registration";
      const data = {
        Name: name,
        Email: email,
        Password: password,
        PhoneNo: phoneNo,
      };

      await axios
        .post(url, data)
        .then((response) => {
          clear();
          const dt = response.data;
          alert(dt.statusMessage);
        })
        .catch((error) => {
          console.log(error);
        });
    }
  };

  const clear = () => {
    setName("");
    setEmail("");
    setPassword("");
    setPhoneNo("");
  };

  return (
    <>
      <section
        style={{
          height: "100vh",
        }}
        className="bg-dark"
      >
        <div className="container py-5 h-100">
          <div className="row d-flex justify-content-center align-items-center h-100">
            <div className="col">
              <div
                className="card card-registration my-4"
                style={{
                  borderRadius: "1.2rem",
                }}
              >
                <div className="row g-0">
                  <div
                    className="col-xl-6 d-xl-block"
                    style={{
                      display: "flex",
                      alignItems: "center",
                      justifyContent: "center",
                    }}
                  >
                    <img
                      src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/draw1.webp"
                      alt="Sample photo"
                      className="img-fluid"
                      style={{
                        borderTopLeftRadius: ".25rem",
                        borderBottomLeftRadius: ".25rem",
                        paddingTop: "120px",
                      }}
                    />
                  </div>
                  <div className="col-xl-6">
                    <div className="card-body p-md-5 text-black">
                      <h3 className="mb-5 text-uppercase">SignUp</h3>

                      <div className="form-outline mb-4">
                        <label className="form-label" htmlFor="form3Example9">
                          Name
                        </label>
                        <input
                          type="text"
                          id="form3Example9"
                          placeholder="Enter Name"
                          className="form-control form-control-lg"
                          value={name}
                          onChange={(e) => setName(e.target.value)}
                        />
                      </div>

                      <div className="form-outline mb-4">
                        <label className="form-label" htmlFor="form3Example90">
                          Email
                        </label>
                        <input
                          type="text"
                          id="form3Example90"
                          placeholder="Enter Email"
                          className="form-control form-control-lg"
                          value={email}
                          onChange={(e) => setEmail(e.target.value)}
                        />
                      </div>

                      <div className="form-outline mb-4">
                        <label className="form-label" htmlFor="form3Example99">
                          Phone
                        </label>
                        <input
                          type="text"
                          id="form3Example99"
                          placeholder="Enter Phone"
                          className="form-control form-control-lg"
                          value={phoneNo}
                          onChange={(e) => setPhoneNo(e.target.value)}
                        />
                      </div>

                      <div className="form-outline mb-4">
                        <label className="form-label" htmlFor="form3Example97">
                          Password
                        </label>
                        <input
                          type="password"
                          id="form3Example97"
                          placeholder="Enter Password"
                          className="form-control form-control-lg"
                          value={password}
                          onChange={(e) => setPassword(e.target.value)}
                        />
                      </div>
                      <a href="/" className="text-black">
                        Already have an account?
                      </a>
                      <div className="d-flex justify-content-end pt-3">
                        <button
                          style={{
                            width: "100%",
                          }}
                          type="button"
                          className="btn btn-warning btn-lg ms-2"
                          onClick={() => handleSave()}
                        >
                          SignUp
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
}

export default Registration;
