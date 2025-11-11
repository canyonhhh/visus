require 'rails_helper'

RSpec.describe "/students", type: :request do

  let(:valid_attributes) { { full_name: "John Doe" } }

  let(:invalid_attributes) { { full_name: nil } }

  describe "GET /index" do
    it "renders a successful response" do
      Student.create! valid_attributes
      get students_url
      expect(response).to be_successful
    end
  end

  describe "GET /show" do
    it "renders a successful response" do
      student = Student.create! valid_attributes
      get student_url(student)
      expect(response).to be_successful
    end
  end

  describe "GET /new" do
    it "renders a successful response" do
      get new_student_url
      expect(response).to be_successful
    end
  end

  describe "GET /edit" do
    it "renders a successful response" do
      student = Student.create! valid_attributes
      get edit_student_url(student)
      expect(response).to be_successful
    end
  end

  describe "POST /create" do
    context "with valid parameters" do
      it "creates a new Student" do
        expect {
          post students_url, params: { student: valid_attributes }
        }.to change(Student, :count).by(1)
      end

      it "redirects to the created student" do
        post students_url, params: { student: valid_attributes }
        expect(response).to redirect_to(student_url(Student.last))
      end
    end

    context "with invalid parameters" do
      it "does not create a new Student" do
        expect {
          post students_url, params: { student: invalid_attributes }
        }.to change(Student, :count).by(0)
      end

      it "renders a response with 422 status (i.e. to display the 'new' template)" do
        post students_url, params: { student: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "DELETE /destroy" do
    it "destroys the requested student" do
      student = Student.create! valid_attributes
      expect {
        delete student_url(student)
      }.to change(Student, :count).by(-1)
    end

    it "redirects to the students list" do
      student = Student.create! valid_attributes
      delete student_url(student)
      expect(response).to redirect_to(students_url)
    end
  end
end
